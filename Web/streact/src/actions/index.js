import axios from "axios";


export const FETCH_TICKETS = "FETCH_TICKETS";
export const CREATE_TICKET = "CREATE_TICKET";

const ROOT_URL = process.env.REACT_APP_API_URL;

export function fetchTickets() {
  const request = axios.get(`${ROOT_URL}/tickets`);
  return {
    type: FETCH_TICKETS,
    payload: request
  };
}

// the callback will be provided by the handler on the form. 
// See ticketsNew where it navigates using history.push
export function createTicket(values, callback) {
  const request = axios
    .post(`${ROOT_URL}/tickets`, values)
    .then(() => callback());

  return {
    type: CREATE_TICKET,
    payload: request
  };
}

  