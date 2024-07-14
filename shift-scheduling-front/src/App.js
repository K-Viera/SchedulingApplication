import logo from "./logo.svg";
import "./App.css";
import Layout from "./components/layout/Layout";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Locations from "./components/locations/Locations";
import ShiftType from "./components/shiftTypes/ShiftType";
import Shifts from "./components/shifts/Shifts";
import Login from "./components/login/Login";

function App() {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route path="/schedules"  Component={Shifts} />
          <Route path="/campus" Component={Locations} />
          <Route path="/types" Component={ShiftType} />
          <Route path="/login" Component={Login} />
        </Routes>
      </Layout>
    </Router>
  );
}

export default App;
