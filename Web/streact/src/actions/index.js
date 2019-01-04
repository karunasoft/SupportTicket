import axios from "axios";
import { FETCH_TICKETS, FETCH_TICKET, CREATE_TICKET, DELETE_TICKET, FETCH_STATIC_DATA, SET_TICKET} from './action-types';

const ROOT_URL = process.env.REACT_APP_API_URL;

export function fetchTickets() {
  const request = axios.get(`${ROOT_URL}/tickets`);
  return {
    type: FETCH_TICKETS,
    payload: request
  };
}

export function fetchStaticData() {
  const request = axios.get(`${ROOT_URL}/staticdata`);
  return {
    type: FETCH_STATIC_DATA,
    payload: request
  };
}

// the callback will be provided by the handler on the form. 
// See ticketsNew where it navigates using history.push
export function createTicket(values, callback) {
  const request = axios
    .post(`${ROOT_URL}/tickets/add`, values)
    .then(() => callback());

  return {
    type: CREATE_TICKET,
    payload: request
  };
}

export function updateTicket(values, callback) {
  const request = axios
    .put(`${ROOT_URL}/tickets`, values)
    .then(() => callback()); // TODO - implement the callback

  return {
    type: CREATE_TICKET,
    payload: request
  };
}

export function fetchTicket(ticketId) {
  const request = axios.get(`${ROOT_URL}/tickets/${ticketId}`);
  return {
    type: FETCH_TICKET,
    payload: request
  };
}

export function deleteTicket(ticketId, callback) {
  const request = axios
  .delete(`${ROOT_URL}/tickets/${ticketId}`)
  .then(() => callback());

  return {
    type: DELETE_TICKET,
    payload: request
  };
}


export function setTicket(ticket) {
  console.log("Fetch ticket from cache");
  return {
    type: SET_TICKET,
    payload: ticket
  };
}


  