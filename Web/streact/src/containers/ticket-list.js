import _ from "lodash";
import React, { Component } from "react";
import { connect } from "react-redux";
import { fetchTickets } from "../actions/index";
import TicketItem from "./ticket-item";
import requireAuth from './auth/requireAuth';

class TicketList extends Component {

  constructor(props){
    super(props);
    this.render = this.render.bind(this);
  }
  componentDidMount() {
    this.props.fetchTickets();
  }

  renderList() {
    return _.map(this.props.tickets, ticket => {
      return (
        <TicketItem key={ticket.ticketId} ticket={ticket}/>
      );
    });
  }

  render() {
    return (
      <div className="st-tickets-panel">
        <div><h2>View Tickets</h2></div>
        <div>
          <p><strong>Note: </strong>Inactive tickets will not be displayed</p>
          <div>
              <p>
                  Download the code from Github <a href="https://github.com/virasana/SupportTicket">here</a>
              </p>
          </div>
        </div>
        <div className="row st-tickets-panel">
            <div className="col-md-12 container st-scrollable-area">
            <table className="table-bordered table-striped table-responsive">
                <thead>
                  <tr className="st-ticketHeader">
                    <th>Ticket ID</th>
                    <th>Description</th>
                    <th>Edit</th>
                    {/* <th>Delete</th> */}
                  </tr>
                </thead>
                <tbody>
                  {this.renderList()}
                </tbody>
            </table>
          </div>
        </div>
      </div>
      )
  }
}

function mapStateToProps(state) {
  if(state.tickets == null){
    return state;
  }

  return { tickets: state.tickets.tickets};
}

export const WrappedTicketList = connect(mapStateToProps, { fetchTickets })(TicketList);
export default requireAuth(WrappedTicketList);