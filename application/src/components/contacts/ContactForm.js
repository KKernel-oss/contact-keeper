import React, { useContext, useState, useEffect } from 'react';
import ContactContext from '../../context/contact/contactContext';

const ContactForm = () => {
  const [contact, setContact] = useState({
    name: '',
    email: '',
    phone: '',
    contactType: 'personal',
  });
  const { name, email, phone, contactType } = contact;
  const contactContext = useContext(ContactContext);
  const { addContact, current, clearCurrent, updateContact } = contactContext;
  useEffect(() => {
    if (current !== null) {
      setContact(current);
    } else {
      setContact({
        name: '',
        email: '',
        phone: '',
        contactType: 'personal',
      });
    }
  }, [contactContext, current]);
  const onTextChanged = (e) => {
    setContact({ ...contact, [e.target.name]: e.target.value });
  };

  const submitForm = (e) => {
    e.preventDefault();
    if (current === null) {
      addContact(contact);
    } else {
      updateContact(contact);
    }

    clearAll();
  };
  const clearAll = () => {
    clearCurrent();
  };
  return (
    <form onSubmit={submitForm}>
      <h2 className='text-primary'>
        {current !== null ? 'Update Contact' : 'Add Contact'}
      </h2>
      <input
        type='text'
        name='name'
        placeholder='Name'
        value={name}
        onChange={onTextChanged}
      />
      <input
        type='email'
        name='email'
        placeholder='Email'
        value={email}
        onChange={onTextChanged}
      />
      <input
        type='text'
        name='phone'
        placeholder='phone'
        value={phone}
        onChange={onTextChanged}
      />
      <h5>Contact Type</h5>
      <input
        type='radio'
        name='contactType'
        value='personal'
        checked={contactType === 'personal'}
        onChange={onTextChanged}
      />{' '}
      Personal{' '}
      <input
        type='radio'
        name='contactType'
        value='professional'
        checked={contactType === 'professional'}
        onChange={onTextChanged}
      />{' '}
      Professional{' '}
      <div>
        <input
          type='submit'
          value={current !== null ? 'Update Contact' : 'Add Contact'}
          className='btn btn=primary btn-block'
        />
      </div>
      {current && (
        <div>
          <button className='btn btn-light btn-block' onClick={clearAll}>
            Clear
          </button>
        </div>
      )}
    </form>
  );
};

export default ContactForm;
