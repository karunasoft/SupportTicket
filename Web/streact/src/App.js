import React from "react";
import { Component } from "react";
import Header from './containers/header';
import { library } from '@fortawesome/fontawesome-svg-core'
import { faExclamation } from '@fortawesome/free-solid-svg-icons'
import { BrowserRouter } from "react-router-dom";
library.add(faExclamation)

export class App extends Component {
  render() {
    const SUPPORT_TICKET_BUILD_REACT_IMAGE="virasana/0.0.0.0"; //default  
    return (
        <BrowserRouter>
            <div id="outer-container" className="st-navbar">
                <Header />
                <div id="page-wrap">
                    {this.props.children}
                </div>
                <div >
                    <footer>
                        <p className="st-footer">Ticket-Track : &copy; Karunasoft Ltd 2018. All rights reserved : Docker: ${SUPPORT_TICKET_BUILD_REACT_IMAGE}</p>
                    </footer>
                </div>
            </div>
        </BrowserRouter>
    );
  }
}

export default App;
