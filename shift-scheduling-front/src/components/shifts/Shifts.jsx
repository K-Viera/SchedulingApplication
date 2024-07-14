import React, { useState, useEffect } from "react";
import Modal from "react-modal";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import { useSelector } from "react-redux";
import { selectAuthToken } from "../../store/authSlice";

const MySwal = withReactContent(Swal);

const customStyles = {
  content: {
    top: "50%",
    left: "50%",
    right: "auto",
    bottom: "auto",
    marginRight: "-50%",
    transform: "translate(-50%, -50%)",
  },
};

Modal.setAppElement(document.getElementById("root"));

const fetchUrl = process.env.REACT_APP_FETCH_URL;
const Shifts = () => {
  const authToken = useSelector(selectAuthToken);
  const [shifts, setShifts] = useState([]);
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [newShift, setNewShift] = useState({
    startDate: "",
    endDate: "",
    shiftTypeId: 0,
    creatorUserId: 0,
    maxUsers: 0,
    totalPrice: 0,
    status: 0,
    placeId: 0,
  });
  const [places, setPlaces] = useState([]);
  const [shiftTypes, setShiftTypes] = useState([]);
  const [formErrors, setFormErrors] = useState({});

  useEffect(() => {
    fetchShifts();
    fetchPlaces();
    fetchShiftTypes();
  }, []);

  const fetchShifts = async () => {
    try {
      const response = await fetch(`${fetchUrl}/shifts`, {
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
      setShifts(data);
      setNewShift({ ...newShift, ["shiftTypeId"]: data[0].shiftTypeId });
    } catch (error) {
      console.error("Error fetching shifts:", error);
    }
  };

  const fetchPlaces = async () => {
    try {
      const response = await fetch(`${fetchUrl}/places`, {
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
      console.log("Places", data);
      setPlaces(data);
      setNewShift({ ...newShift, ["placeId"]: data[0].placeId });
    } catch (error) {
      console.error("Error fetching places:", error);
    }
  };

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
      console.log("Shift Types", data);
      setShiftTypes(data);
    } catch (error) {
      console.error("Error fetching shift types:", error);
    }
  };

  const validateForm = () => {
    const errors = {};
    if (!newShift.startDate) errors.startDate = "Start date is required";
    if (!newShift.endDate) errors.endDate = "End date is required";
    setFormErrors(errors);
    return Object.keys(errors).length === 0;
  };

  const handleCreate = async () => {
    if (!validateForm()) {
      console.error("Validation failed:", formErrors);
      return;
    }
    try {
      console.log("New Shift", newShift);
      const response = await fetch(`${fetchUrl}/shifts`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(newShift),
      });
      if (!response.ok) {
        console.log("Response", response);
        const errorResponse = await response.json();
        console.log("Response JSON", errorResponse);
        let errorMessage = "Please correct the following errors:\n";
        Object.keys(errorResponse.errors).forEach((key) => {
          const messages = errorResponse.errors[key].join("\n");
          errorMessage += `${messages}\n`;
        });
        MySwal.fire({
          icon: "error",
          title: "Oops...",
          text: errorMessage,
        });
      }
      closeModal();
      fetchShifts();
    } catch (error) {
      console.error("Error creating new shift:", error);
    }
  };

  const openModal = () => setModalIsOpen(true);
  const closeModal = () => {
    setModalIsOpen(false);
    setNewShift({
      startDate: "",
      endDate: "",
      shiftTypeId: 0,
      creatorUserId: 0,
      maxUsers: 0,
      totalPrice: 0,
      status: 0,
      placeId: 0,
    });
    setFormErrors({});
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setNewShift({ ...newShift, [name]: value });
  };

  return (
    <>
      <div className="body-content-shifts">
        <h2 className="header">Shifts</h2>
        <button className="button" onClick={openModal}>
          Add New Shift
        </button>

        <ul className="shifts-list">
          {shifts.map((shift, index) => (
            <li key={shift.shiftTypeId} className="list-item">
              {`Shift from ${shift.startDate} to ${shift.endDate}`}
            </li>
          ))}
        </ul>
      </div>
      <Modal
        isOpen={modalIsOpen}
        onRequestClose={closeModal}
        contentLabel="Add New Shift"
        style={customStyles}
      >
        <div className="modal-content">
          <div className="modal-header">
            <h2 className="modal-title">Add New Shift</h2>
            <button onClick={closeModal} className="modal-close-button">
              X
            </button>
          </div>
          <form className="modal-form">
            <label htmlFor="startDate">Start Date:</label>
            <input
              type="datetime-local"
              name="startDate"
              id="startDate"
              value={newShift.startDate}
              onChange={handleChange}
              className="modal-form-input"
            />
            {formErrors.startDate && (
              <p className="error">{formErrors.startDate}</p>
            )}
            <label htmlFor="endDate">End Date:</label>
            <input
              type="datetime-local"
              name="endDate"
              id="endDate"
              value={newShift.endDate}
              onChange={handleChange}
              className="modal-form-input"
            />
            {formErrors.endDate && (
              <p className="error">{formErrors.endDate}</p>
            )}

            <label htmlFor="shiftTypeId">Shift Type:</label>
            <select
              name="shiftTypeId"
              id="shiftTypeId"
              value={newShift.shiftTypeId}
              onChange={handleChange}
              className="modal-form-select"
            >
              {shiftTypes.map((type) => (
                <option key={type.shiftTypeId} value={type.shiftTypeId}>
                  {type.name}
                </option>
              ))}
            </select>
            {formErrors.shiftTypeId && (
              <p className="error">{formErrors.shiftTypeId}</p>
            )}

            <label htmlFor="maxUsers">Max Users:</label>
            <input
              type="number"
              name="maxUsers"
              id="maxUsers"
              value={newShift.maxUsers}
              onChange={handleChange}
              className="modal-form-input"
            />
            {formErrors.maxUsers && (
              <p className="error">{formErrors.maxUsers}</p>
            )}

            <label htmlFor="placeId">Place:</label>
            <select
              name="placeId"
              id="placeId"
              value={newShift.placeId}
              onChange={handleChange}
              className="modal-form-select"
            >
              {places.map((place) => (
                <option key={place.placeId} value={place.placeId}>
                  {place.name}
                </option>
              ))}
            </select>
            {formErrors.placeId && (
              <p className="error">{formErrors.placeId}</p>
            )}
            <button
              type="button"
              onClick={handleCreate}
              className="modal-form-button"
            >
              Create Shift
            </button>
          </form>
          <div className="modal-footer">
            <button onClick={closeModal} className="modal-form-button">
              Close
            </button>
          </div>
        </div>
      </Modal>
    </>
  );
};

export default Shifts;
