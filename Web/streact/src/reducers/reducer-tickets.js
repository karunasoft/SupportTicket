import _ from "lodash";
import { 
  FETCH_TICKETS, 
  FETCH_STATIC_DATA, 
  FETCH_TICKET, 
  SET_TICKET,
  DELETE_TICKET
} from "../actions/action-types";

export default function(state = null, action) {
  let result;
  switch (action.type) {
    case FETCH_TICKETS:
      result = {...state, tickets: _.mapKeys(action.payload.data, "ticketId") };
      return result;
      case FETCH_TICKET:
      result = {...state, ticket: action.payload.data};
      return result;
      case SET_TICKET:
      result = {...state, ticket: action.payload};
      return result;
    case DELETE_TICKET:
      result = {...state, ticket: null};
      return result;
    case FETCH_STATIC_DATA:
      result = {...state, staticData: action.payload.data};
      return result;
    default:
      return state;
  }
}



