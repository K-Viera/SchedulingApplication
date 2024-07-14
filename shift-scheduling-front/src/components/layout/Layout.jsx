import React from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types"; // Import PropTypes module
import "./layout.css";

const Layout = ({ children }) => {
  return (
    <div>
      <header className="header">
        <div className="header-content">
          <span className="page-name">Scheduling</span>
          <nav>
            <Link to="/schedules" className="nav-link">
              Schedules
            </Link>
            <Link to="/campus" className="nav-link"> {/* Updated route */}
              Campus
            </Link>
            <Link to="/types" className="nav-link"> {/* Updated route */}
              Types
            </Link>
            <Link to="/login" className="login-button">
              Login  
            </Link>
          </nav>
        </div>
      </header>
      <main>{children}</main>
      <footer className="footer">
        <p>@K-Viera</p>
      </footer>
    </div>
  );
};

// Moved propTypes definition outside the component function
Layout.propTypes = {
  children: PropTypes.node.isRequired,
};

export default Layout;