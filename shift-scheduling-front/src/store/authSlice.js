import { createSlice } from '@reduxjs/toolkit';

export const authSlice = createSlice({
  name: 'auth',
  initialState: {
    authenticationToken: null,
  },
  reducers: {
    setAuthToken: (state, action) => {
      state.authenticationToken = action.payload;
    },
    clearAuthToken: (state) => {
      state.authenticationToken = null;
    },
  },
});

export const { setAuthToken, clearAuthToken } = authSlice.actions;

export const selectAuthToken = (state) => state.auth.authenticationToken;

export default authSlice.reducer;