import './App.css';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import Navbar from './components/layout/Navbar';
import Home from './components/pages/Home';
import About from './components/pages/About';
import { Fragment } from 'react';

import ContactState from './context/contact/ContactState';

function App() {
  return (
    <ContactState>
      <Router>
        <Fragment>
          <Navbar></Navbar>
          <div className='container'>
            <Switch>
              <Route exact path='/' component={Home}></Route>
              <Route exact path='/about' component={About}></Route>
            </Switch>
          </div>
        </Fragment>
      </Router>
    </ContactState>
  );
}

export default App;
