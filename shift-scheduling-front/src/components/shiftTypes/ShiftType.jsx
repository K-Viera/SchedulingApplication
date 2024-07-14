import React, { useState, useEffect } from "react";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import { useSelector } from "react-redux";
import { selectAuthToken } from "../../store/authSlice";

const MySwal = withReactContent(Swal);

const fetchUrl = process.env.REACT_APP_FETCH_URL;
const ShiftType = () => {
  const authToken = useSelector(selectAuthToken);
  const [shiftTypes, setShiftTypes] = useState([]);
  const [editShiftTypeId, setEditShiftTypeId] = useState(null);
  const [newShiftType, setNewShiftType] = useState({
    name: "",
    description: "",
    price: "",
    nightPrice: "",
    holidayPrice: "",
  });
  const [editShiftType, setEditShiftType] = useState({
    name: "",
    description: "",
    price: "",
    nightPrice: "",
    holidayPrice: "",
  });

  useEffect(() => {
    fetchShiftTypes();
  }, []);

  const fetchShiftTypes = async () => {
    try {
      const response = await fetch(`${fetchUrl}/shiftTypes`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${authToken}`, // Add the Authorization header
        },
      });
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      const data = await response.json();
      setShiftTypes(data);
    } catch (error) {
      console.error("Error fetching shift types:", error);
    }
  };

  const handleEdit = (id, shiftType) => {
    setEditShiftTypeId(id);
    setEditShiftType(shiftType);
  };

  const handleSaveEdit = async (id) => {
    try {
      const response = await fetch(`${fetchUrl}/shiftTypes/${id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(editShiftType),
      });

      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      setEditShiftTypeId(null);
      fetchShiftTypes();
    } catch (error) {
      console.error("Error updating shift type:", error);
    }
  };

  const handleDelete = async (id) => {
    try {
      const response = await fetch(`${fetchUrl}/shiftTypes/${id}`, {
        method: "DELETE",
      });
      console.log("result", response);
      if (!response.ok) {
        const errorMessage = await response.text();
        MySwal.fire({
          icon: "error",
          title: "Oops...",
          text: errorMessage,
        });
      }
      fetchShiftTypes();
    } catch (error) {
      console.error("Error deleting shift type:", error);
    }
  };

  const handleCreate = async () => {
    try {
      const response = await fetch(`${fetchUrl}/shiftTypes`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          'Authorization': `Bearer ${authToken}`,
        },
        body: JSON.stringify(newShiftType),
      });
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      setNewShiftType({
        name: "",
        description: "",
        price: "",
        nightPrice: "",
        holidayPrice: "",
      });
      fetchShiftTypes();
    } catch (error) {
      console.error("Error creating new shift type:", error);
    }
  };

  return (
    <div className="body-content-shifts-type">
      <h2 className="header">Shift Types</h2>
      <ul className="shift-types-list">
        {shiftTypes.map((shiftType) => (
          <li key={shiftType.shiftTypeId} className="shift-type-item">
            {editShiftTypeId === shiftType.shiftTypeId ? (
              <>
                <input
                  type="text"
                  className="input-field"
                  value={editShiftType.name}
                  onChange={(e) =>
                    setEditShiftType({ ...editShiftType, name: e.target.value })
                  }
                  placeholder="Name"
                />
                <input
                  type="text"
                  className="input-field"
                  value={editShiftType.description}
                  onChange={(e) =>
                    setEditShiftType({
                      ...editShiftType,
                      description: e.target.value,
                    })
                  }
                  placeholder="Description"
                />
                <input
                  type="number"
                  className="input-field"
                  value={editShiftType.price}
                  onChange={(e) =>
                    setEditShiftType({
                      ...editShiftType,
                      price: e.target.value,
                    })
                  }
                  placeholder="Price"
                />
                <input
                  type="number"
                  className="input-field"
                  value={editShiftType.nightPrice}
                  onChange={(e) =>
                    setEditShiftType({
                      ...editShiftType,
                      nightPrice: e.target.value,
                    })
                  }
                  placeholder="Night Price"
                />
                <input
                  type="number"
                  className="input-field"
                  value={editShiftType.holidayPrice}
                  onChange={(e) =>
                    setEditShiftType({
                      ...editShiftType,
                      holidayPrice: e.target.value,
                    })
                  }
                  placeholder="Holiday Price"
                />
                <button
                  className="button"
                  onClick={() => handleSaveEdit(shiftType.shiftTypeId)}
                >
                  Save
                </button>
              </>
            ) : (
              <>
                <div>
                  {shiftType.name} - {shiftType.description}
                </div>
                <div>
                  Price: {shiftType.price}, Night Price: {shiftType.nightPrice},
                  Holiday Price: {shiftType.holidayPrice}
                </div>
                <div className="button-group">
                  <button
                    className="button edit-button"
                    onClick={() => handleEdit(shiftType.shiftTypeId, shiftType)}
                  >
                    Edit
                  </button>
                  <button
                    className="button delete-button"
                    onClick={() => handleDelete(shiftType.shiftTypeId)}
                  >
                    <i className="fas fa-trash"></i>
                  </button>
                </div>
              </>
            )}
          </li>
        ))}
      </ul>
      <div className="add-shift-type">
        <input
          type="text"
          className="input-field"
          value={newShiftType.name}
          onChange={(e) =>
            setNewShiftType({ ...newShiftType, name: e.target.value })
          }
          placeholder="New Shift Type Name"
        />
        <input
          type="text"
          className="input-field"
          value={newShiftType.description}
          onChange={(e) =>
            setNewShiftType({ ...newShiftType, description: e.target.value })
          }
          placeholder="Description"
        />
        <input
          type="number"
          className="input-field"
          value={newShiftType.price}
          onChange={(e) =>
            setNewShiftType({ ...newShiftType, price: e.target.value })
          }
          placeholder="Price"
        />
        <input
          type="number"
          className="input-field"
          value={newShiftType.nightPrice}
          onChange={(e) =>
            setNewShiftType({ ...newShiftType, nightPrice: e.target.value })
          }
          placeholder="Night Price"
        />
        <input
          type="number"
          className="input-field"
          value={newShiftType.holidayPrice}
          onChange={(e) =>
            setNewShiftType({ ...newShiftType, holidayPrice: e.target.value })
          }
          placeholder="Holiday Price"
        />
        <button className="button" onClick={handleCreate}>
          Add Shift Type
        </button>
      </div>
    </div>
  );
};

export default ShiftType;
