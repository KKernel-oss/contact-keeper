import React, { useContext } from 'react';
import PropTypes from 'prop-types';
import ContactContext from '../../context/contact/contactContext';

const ContactItem = ({ contact }) => {
  const { id, name, email, phone, contactType } = contact;
  const contactContext = useContext(ContactContext);
  const { deleteContact, setCurrent, clearCurrent } = contactContext;
  const removeContact = (e) => {
    deleteContact(id);
    clearCurrent();
  };
  const setCurrentContact = () => {
    setCurrent(contact);
  };
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
            <i className='far fa-envelope'></i> {email}
          </li>
        )}
        {phone && (
          <li>
            <i className='fas fa-mobile-alt'></i> {phone}
          </li>
        )}
      </ul>
      <p>
        <button className='btn btn-dark btn-sm' onClick={setCurrentContact}>
          Edit
        </button>
        <button className='btn btn-danger btn-sm' onClick={removeContact}>
          Remove
        </button>
      </p>
    </div>
  );
};

ContactItem.propTypes = {
  contact: PropTypes.object.isRequired,
};

export default ContactItem;
