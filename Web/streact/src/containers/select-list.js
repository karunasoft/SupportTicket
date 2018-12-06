// https://stackoverflow.com/questions/40075281/how-to-create-custom-dropdown-field-component-with-redux-form-v6
import React from 'react';

class DropDownSelect extends React.Component { // eslint-disable-line react/prefer-stateless-function

  renderSelectOptions = (item) => (
    <option key={item[Object.keys(item)[0]]} value={item[Object.keys(item)[1]]}>{item[Object.keys(item)[1]]}</option>
  )

  render() {
    const { input, label, className, meta: { touched, error }} = this.props;
    
    return (
      <div>
        <label htmlFor={label}>{label}</label>
        <select {...input} className={className}>
          <option value="">Select</option>
          {this.props.items.map(this.renderSelectOptions)}
        </select>
        <div className="text-help">
            {touched ? error : ""}
          </div>
      </div>
    );
  }
}

export default DropDownSelect;