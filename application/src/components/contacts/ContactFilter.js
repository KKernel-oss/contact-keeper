import React, { useContext, useState, useEffect } from 'react';
import ContactContext from '../../context/contact/contactContext';

const ContactFilter = () => {
  const contactContext = useContext(ContactContext);
  const { filterContacts, clearFilter, filtered } = contactContext;
  const [text, setText] = useState('');

  const onChangedText = (e) => {
    if (e.target.value) {
      setText(e.target.value);
      filterContacts(text);
    } else {
      clearFilter();
    }
  };
  return (
    <form>
      <input
        type='text'
        name='text'
        placeholder='Filter Contacts ...'
        onChange={onChangedText}
      />
    </form>
  );
};

export default ContactFilter;
