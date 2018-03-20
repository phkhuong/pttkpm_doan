

import React from 'react';
// import PropTypes from 'prop-types';
import { connect } from 'react-redux';

import { withRouter } from 'react-router-dom';
import reducer from './reducer';

import { increaseCounter } from './actions';
import Counter from '../../components/Counter';


/* eslint-disable react/prefer-stateless-function */
export class CounterContainer extends React.Component {
  

  

  render() {
    // console.log(this.props);
    return (
      <div>
        <Counter {...this.props} />
        <p>aaa</p>

        <button onClick={this.props.add}>ADD</button>
      </div>
    );
  }
}

function mapStateToProps(state) {
  // console.log(state);
  return {
    counter: state.CounterContainerState.counter
  };
}

function mapDispatchToProps(dispatch) {
  return {
    add: () => dispatch(increaseCounter()),
    
  };
}



// export default withRouter(connect(mapStateToProps, mapDispatchToProps)(CounterContainer));
export default connect(mapStateToProps, mapDispatchToProps)(CounterContainer);
