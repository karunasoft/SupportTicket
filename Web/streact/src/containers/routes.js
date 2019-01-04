import React, {Component} from 'react';
import { Route, Switch } from "react-router-dom";
import TicketList from "./ticket-list";
import TicketsNew from "./tickets-new";
import TicketsEdit from "./tickets-edit";
import Signin from './auth/Signin';
import Signup from './auth/Signup';
import Signout from './auth/Signout';


export default class Routes extends Component {
    render() {
        return (
                <Switch>
                    <Route path="/signup" component={Signup} />
                    <Route path="/signout" component={Signout} />
                    <Route path="/signin" component={Signin} />
                    <Route path="/tickets/edit/:id" component={TicketsEdit} />
                    <Route path="/tickets/new" component={TicketsNew} />
                    <Route path="/tickets" component={TicketList} />
                    <Route path="/" component={TicketList} />
                </Switch>
        )
    }
}
