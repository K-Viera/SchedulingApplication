import React, { useState, useEffect } from "react";
import Swal from "sweetalert2";
import withReactContent from 'sweetalert2-react-content';


const MySwal = withReactContent(Swal);

const fetchUrl = process.env.REACT_APP_FETCH_URL;
const Locations = () => {
  const [places, setPlaces] = useState([]);
  const [editPlaceId, setEditPlaceId] = useState(null);
  const [newPlaceName, setNewPlaceName] = useState("");
  const [editPlaceName, setEditPlaceName] = useState("");

  useEffect(() => {
    fetchPlaces();
  }, []);

  const fetchPlaces = async () => {
    try {
      const response = await fetch(`${fetchUrl}/places`);
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      const data = await response.json();
      console.log("terst", data);
      setPlaces(data);
    } catch (error) {
      console.error("Error fetching places:", error);
    }
  };

  const handleEdit = (id, name) => {
    setEditPlaceId(id);
    setEditPlaceName(name);
  };

  const handleSaveEdit = async (id) => {
    try {
      const response = await fetch(`${fetchUrl}/places/${id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ name: editPlaceName }),
      });

      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      setEditPlaceId(null);
      fetchPlaces();
    } catch (error) {
      console.error("Error updating place:", error);
    }
  };

  const handleDelete = async (id) => {
    try {
      const response = await fetch(`${fetchUrl}/places/${id}`, {
        method: "DELETE",
      });

      if (!response.ok) {
        const errorMessage = await response.text();
        MySwal.fire({
          icon: 'error',
          title: 'Oops...',
          text: errorMessage,
        });
      }
      fetchPlaces();
    } catch (error) {

      console.error("Error deleting place:", error);
    }
  };

  const handleCreate = async () => {
    try {
      const response = await fetch(`${fetchUrl}/places`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ name: newPlaceName }),
      });
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      setNewPlaceName("");
      fetchPlaces();
    } catch (error) {
      console.error("Error creating new place:", error);
    }
  };

  return (
    <div className="body-content">
      <h2 className="header">Campus</h2>
      <ul className="places-list">
        {places.map((place) => (
          <li key={place.placeId} className="place-item">
            {editPlaceId === place.placeId ? (
              <>
              <div>{place.placeId}</div>
                <input
                  type="text"
                  className="input-field"
                  value={editPlaceName}
                  onChange={(e) => setEditPlaceName(e.target.value)}
                />
                <button
                  className="button"
                  onClick={() => handleSaveEdit(place.placeId)}
                >
                  Save
                </button>
              </>
            ) : (
              <>
                {place.name}
                <div className="button-group">
                  <button
                    className="button edit-button"
                    onClick={() => handleEdit(place.placeId, place.name)}
                  >
                    Edit
                  </button>
                  <button
                    className="button delete-button"
                    onClick={() => handleDelete(place.placeId)}
                  >
                    <i className="fas fa-trash"></i> 
                  </button>
                </div>
              </>
            )}
          </li>
        ))}
      </ul>
      <div className="add-place">
        <input
          type="text"
          className="input-field"
          value={newPlaceName}
          onChange={(e) => setNewPlaceName(e.target.value)}
          placeholder="New place name"
        />
        <button className="button" onClick={handleCreate}>
          Add Place
        </button>
      </div>
    </div>
  );
};

export default Locations;
