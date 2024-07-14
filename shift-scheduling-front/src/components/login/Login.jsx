import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { setAuthToken } from '../../store/authSlice';
import Swal from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';

const MySwal = withReactContent(Swal);

const fetchUrl = process.env.REACT_APP_FETCH_URL;
const Login = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const dispatch = useDispatch();

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch(fetchUrl+"/login", {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email, password }),
      });
      console.log(response);
      if (!response.ok) {
        const errorMessage = await response.text();
        MySwal.fire({
          icon: 'error',
          title: 'Login failed',
          text: errorMessage,
          showConfirmButton: false,
          timer: 1500,
        });        
      }else{
        const textResponse = await response.text();
        dispatch(setAuthToken(textResponse));
        MySwal.fire({
          icon: 'success',
          title: 'Login successful',
          showConfirmButton: false,
          timer: 1500,
        });
      }
    } catch (error) {
      console.error('Login error:', error);
    }
  };

  return (
    <div className="login-container">
      <form onSubmit={handleSubmit} className="login-form">
        <div className="form-group">
          <label htmlFor="email" className="form-label">Email:</label>
          <input
            type="email"
            id="email"
            className="form-input"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="password" className="form-label">Password:</label>
          <input
            type="password"
            id="password"
            className="form-input"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <button type="submit" className="form-button">Login</button>
      </form>
    </div>
  );
};

export default Login;