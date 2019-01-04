import { fetchTickets } from '../index';
import { FETCH_TICKETS } from '../action-types';

describe('fetchTickets', () => {
  it('has the correct type', () => {
    const action = fetchTickets();

    expect(action.type).toEqual(FETCH_TICKETS);
  });

  it('payload is type of promise', () => {
    const action = fetchTickets();
    expect(typeof action.payload.then == 'function').toEqual(true);
  });
});