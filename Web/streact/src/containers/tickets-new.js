import React, { Component } from "react";
import { Field, reduxForm } from "redux-form";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { createTicket, fetchStaticData } from "../actions/index";
import DropDownSelect from "./select-list";
import { bindActionCreators } from "redux";
import requireAuth from './auth/requireAuth'
import StTextBox from './form-components/st-text-box';
import StTextArea from './form-components/st-text-area';
import StCheckBox from './form-components/st-check-box';

class TicketsNew extends Component {
  constructor (props) {
    super(props)
    this.render = this.render.bind(this);
  }

  componentDidMount() {
    this.props.fetchStaticData();
  }  
  
  // See the callback - it will be called by the action creator (see the action creator)
  onSubmit(values) {
    this.props.createTicket(values, () => { 
      this.props.history.push("/");
    });
  }

  render() {
    const { handleSubmit, invalid } = this.props

    return (
      <div className="st-tickets-panel">
        <div><h2>New Ticket</h2></div>
        <form onSubmit={handleSubmit(this.onSubmit.bind(this))}>
          <Field
            label="Description For Ticket"
            name="description"
            component={StTextBox}
          />
          <Field
            label="Problem"
            name="problem"
            component={StTextArea}
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
   // if(Boolean(state.tickets.severities) && Boolean(state.tickets.severities.staticData)){
      return { severities: state.tickets.staticData.severities, products: state.tickets.staticData.products };
    //}
  }
  catch(err) {
    return {};
  }
}

function mapDispatchToProps(dispatch) {
  // Whenever fetchStaticData is called, the result should be passed
  // to all of our reducers
  return bindActionCreators({ createTicket, fetchStaticData }, dispatch);
}

TicketsNew = connect(mapStateToProps, mapDispatchToProps)(TicketsNew);
// Could also use destructuring as follows:
// TicketsNew = connect(mapStateToProps, { createTicket, fetchStaticData })(TicketsNew);

export default requireAuth(reduxForm({
  validate,
  form: "TicketsNewForm"
})(TicketsNew)); 