import React from 'react';
import PropTypes from 'prop-types';

const ContactItem = ({ contact }) => {
  const { id, name, email, phone, contactType } = contact;
  return (
    <div className='card bg-light'>
      <h3 className='text-primary text-left'>
        {name}{' '}
        <span
          style={{ float: 'right' }}
          className={
            'cap badge ' +
            (contactType === 'professional' ? 'badge-success' : 'badge-primary')
          }
        >
          {contactType}
        </span>
      </h3>
      <ul className='list'>
        {email && (
          <li>
            <i class='far fa-envelope'></i> {email}
          </li>
        )}
        {phone && (
          <li>
            <i class='fas fa-mobile-alt'></i> {email}
          </li>
        )}
      </ul>
      <p>
        <button className='btn btn-dark btn-sm'>Edit</button>
        <button className='btn btn-danger btn-sm'>Remove</button>
      </p>
    </div>
  );
};

ContactItem.propTypes = {
  contact: PropTypes.object.isRequired,
};

export default ContactItem;
