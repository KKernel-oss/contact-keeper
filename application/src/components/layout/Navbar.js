import React from 'react';
import PropTypes from 'prop-types';
import { Link } from 'react-router-dom';

export const Navbar = ({ title, icon }) => {
  return (
    <div className='navbar bg-primary'>
      <h2>
        <i className={icon}></i> {title}
      </h2>
      <ul>
        <li>
          <Link to='/'>Home</Link>
        </li>
        <li>
          <Link to='/about'>About</Link>
        </li>
      </ul>
    </div>
  );
};

Navbar.propTypes = {
  title: PropTypes.string.isRequired,
  icon: PropTypes.string.isRequired,
};

Navbar.defaultProps = {
  title: 'Focus | People Finder',
  icon: 'far fa-id-card',
};

export default Navbar;
