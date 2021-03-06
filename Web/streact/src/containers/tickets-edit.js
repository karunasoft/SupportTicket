import React, { Component } from "react";
import { Field, reduxForm } from "redux-form";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { updateTicket, fetchStaticData, fetchTicket, setTicket } from "../actions/index";
import DropDownSelect from "./select-list";
import { bindActionCreators } from "redux";
import requireAuth from './auth/requireAuth'
import StTextBox from './form-components/st-text-box';
import StTextArea from './form-components/st-text-area';
import StCheckBox from './form-components/st-check-box';


class TicketsEdit extends Component {
  constructor (props) {
    super(props)
    this.render = this.render.bind(this);
  }

  componentDidMount() {
    
    this.props.fetchStaticData();
     
   // if(this.props.location.state){
      //this.props.setTicket(this.props.location.state);
    //} else {
      this.props.fetchTicket(this.props.match.params.id);
  //  }
  }  
  // See the callback - it will be called by the action creator (see the action creator)
  onSubmit(values) {
    this.props.updateTicket(values, () => { 
      this.props.history.push("/");
    });
  }

  render() {
    const { handleSubmit, invalid } = this.props

    return (
      <div className="st-tickets-panel">
        <div><h2>Edit Ticket</h2></div>
        <form onSubmit={handleSubmit(this.onSubmit.bind(this))}>
          <div><label>Ticket ID</label>{this.props.initialValues ? this.props.initialValues.ticketId: ""}</div>
          <Field
            label="Description For Ticket"
            name="description"
            component={StTextBox}
          />
          <Field
            label="Problem"
            name="problem"
            component={StTextArea}
            type="textarea"
          />
          <Field
            label="Active"
            name="active"
            component={StCheckBox}
            type="checkbox"
          />
          <Field
            name="productId"
            label="Product"
            component={DropDownSelect}
            items={this.props.products == null ? [] : this.props.products}
            className="form-control"
          />
          <Field
            name="severityId"
            label="Severity"
            component={DropDownSelect}
            items={this.props.severities == null ? [] : this.props.severities}
            className="form-control"
          />
          <button type="submit" className="btn btn-basic st-formbutton" disabled={invalid}>Submit</button>
          <Link to="/" className="btn btn-danger st-formbutton">Cancel</Link>
        </form>
      </div>
    );
  }
}



function validate(values) {
  const errors = {};

  // Validate the inputs from 'values'
  if (!values.description) {
    errors.description = "Enter a description";
  }
  if (!values.problem) {
    errors.problem = "Provide problem details";
  }

  if (!values.severityId) {
    errors.severityId = "Select severity";
  }

  if (!values.productId) {
    errors.productId = "Select product";
  }

  return errors;
}

function mapStateToProps(state) {
  // Whatever is returned will show up as props
  // inside of TicketList
  
  if(state.tickets == null) {
    return {};
  }
  
  try {
      let result =  { 
        initialValues: state.tickets.ticket, 
        severities: state.tickets.staticData.severities,
        products: state.tickets.staticData.products,
      };

      return result;

  }
  catch(err) {
    return {};
  }
}

function mapDispatchToProps(dispatch) {
  // Whenever fetchStaticData is called, the result should be passed
  // to all of our reducers
  return bindActionCreators({ 
    updateTicket, 
    fetchStaticData, 
    fetchTicket, 
    setTicket }, dispatch);
}

TicketsEdit = reduxForm({
  validate,
  form: "TicketsEditForm",
  enableReinitialize: true,
  keepDirtyOnReinitialize: true
})(TicketsEdit); 

export default requireAuth(connect(mapStateToProps, mapDispatchToProps)(TicketsEdit));
// Could also use destructuring as follows:
// TicketsEdit = connect(mapStateToProps, { updateTicket, fetchStaticData })(TicketsEdit);

