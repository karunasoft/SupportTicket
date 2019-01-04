import React from 'react';
import Root from '../root';
import App from '../App';
import { WrappedTicketList as TicketList } from '../containers/ticket-list';
import moxios from 'moxios';
import Enzyme from 'enzyme';
import {mount} from 'enzyme';
import Adapter from 'enzyme-adapter-react-16';


Enzyme.configure({ adapter: new Adapter() });  // setupTests.js not working

const ROOT_URL = process.env.REACT_APP_API_URL;

describe('view tickets', ()=> {
    beforeEach(()=>{
        moxios.install();
        moxios.stubRequest(`${ROOT_URL}/tickets`, {
            status: 200,
            response: [
                { ticketId: 1, productId: 1, severityId: 1, problem: "Pr1", description: "D1", active: true}, 
                { ticketId: 2, productId: 2, severityId: 2, problem: "Pr2", description: "D2", active: true},
                { ticketId: 3, productId: 3, severityId: 3, problem: "Pr3", description: "D3", active: true},
                
        ]}); 
    });

    afterEach(()=>{
        moxios.uninstall();
    });

    it('should fetch a list of tickets and display the ticket columns for each one', (done)=>{
        const wrapped = mount(
            <Root>
                <App>
                    <TicketList />
                </App>
            </Root>
        );

        moxios.wait(() => {
            wrapped.update(); 
            expect(wrapped.find('.st-ticketId-column').length).toEqual(3);    
            done();
            wrapped.unmount();
        });
    });

    it('should display a header row with Ticket ID, Description, Edit and Delete columns', (done)=>{
        const wrapped = mount(
            <Root>
                <App>
                    <TicketList />
                </App>
            </Root>
        );

        moxios.wait(() => {
            wrapped.update(); 
            const rows = wrapped.find('.st-ticketHeader');
            expect(rows.find('th').length).toEqual(3);
            rows.forEach(row => {
                expect(row.find('th').at(0).render().text()).toEqual('Ticket ID');
                expect(row.find('th').at(1).render().text()).toEqual('Description');
                expect(row.find('th').at(2).render().text()).toEqual('Edit');
            });
            
            done();
            wrapped.unmount();
        });
    });


    it('should display Edit and Delete buttons, in order, in each row', (done)=>{
        const wrapped = mount(
            <Root>
                <App>
                    <TicketList />
                </App>
            </Root>
        );

        moxios.wait(() => {
            wrapped.update(); 

            const rows = wrapped.find('.st-ticketRow');
            expect(rows.length).toBeGreaterThan(0);
            rows.forEach(row => {
                expect(row.find('button').at(0).render().text()).toEqual('Edit');
            });
            
            done();
            wrapped.unmount();
        });
    });


});






