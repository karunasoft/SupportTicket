import React, {Component} from 'react';
import { Field, reduxForm } from "redux-form";
import { compose } from 'redux';
import { connect } from 'react-redux';
import * as actions from '../../actions/auth-actions';
import StTextBox from '../form-components/st-text-box';
import StPassword from '../form-components/st-password';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core'
import { faExclamation } from '@fortawesome/free-solid-svg-icons'

library.add(faExclamation);


export class Signin extends Component {
    onSubmit = formProps => {
        debugger;

        this.props.signin(formProps, () => {
            console.log("SIGNIN - Pushing /tickets", this.props.history);
            
            this.props.history.push('/tickets');
        });
      };

    render(){
        const { handleSubmit, invalid } = this.props;
        return (
                <div>
                    <div><h2>Sign In</h2></div>
                    <form onSubmit={handleSubmit(this.onSubmit)}>
                        <fieldset>
                            <Field
                                label="User Name"
                                name="username"
                                component={StTextBox}
                            />
                        </fieldset>
                        <fieldset>
                            <Field
                                label="Password"
                                name="password"
                                component={StPassword}
                            />
                        </fieldset>
                        <div className="st-texthelp">{(this.props.errorMessage && <FontAwesomeIcon icon="exclamation" className="st-exclamation" />)}{this.props.errorMessage}</div>
                        <button type="submit" className="btn btn-basic st-formbutton" disabled={invalid}>Submit</button>
                        <Link to="/signup"><button className="btn btn-default st-formbutton">Sign Up</button></Link>
                    </form>
                </div>
        )
    }
}

function validate(values) {
    const errors = {};
  
    if (!values.username) {
        errors.username = "Enter User Name";
      }
  
    if (!values.password) {
      errors.password = "Enter Password";
    }
  
    return errors;
  }

function mapStateToProps(state) {
    return { errorMessage: state.auth.errorMessage };
}

export default compose(
    connect(mapStateToProps, actions),
    reduxForm({ form: 'signin', validate })
  )(Signin);