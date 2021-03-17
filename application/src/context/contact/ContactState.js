// Contains methods that alter various states of the application
import React, { useReducer } from 'react';
import uuid from 'uuid';
import ContactContext from './contactContext';
import contactReducer from './contactReducer';
import {
  ADD_CONTACT,
  DELETE_CONTACT,
  SET_CURRENT,
  CLEAR_CURRENT,
  UPDATE_CONTACT,
  FILTER_CONTACTS,
  CLEAR_FILTER,
} from '../Types';
import contactContext from './contactContext';

const ContactState = (props) => {
  const initialState = {
    contacts: [
      {
        id: 1,
        name: 'Jill Johnson',
        email: 'JJohnson@gmail.com',
        phone: '18763256787',
        contactType: 'personal',
      },
      {
        id: 2,
        name: 'Bill Markie',
        email: 'BMarkie@gmail.com',
        phone: '18763223287',
        contactType: 'personal',
      },
      {
        id: 3,
        name: 'Joan Brown',
        email: 'JBrown@gmail.com',
        phone: '18761223287',
        contactType: 'professional',
      },
    ],
  };

  const [state, dispatch] = useReducer(contactReducer, initialState);

  /* Actions required for object */

  //Add Contact

  //Delete Contact

  //Set Current Contact

  //Clear Current Contact

  //Update Contact

  //Filter Contacts

  //Clear Filtered Contacts

  return (
    <contactContext.Provider
      value={{
        contacts: state.contacts,
      }}
    >
      {props.children}
    </contactContext.Provider>
  );
};

export default ContactState;
