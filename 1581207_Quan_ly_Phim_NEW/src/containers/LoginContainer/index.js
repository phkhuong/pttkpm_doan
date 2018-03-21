import React from 'react';
import { connect } from "react-redux";
import { Redirect } from "react-router";

import { requestLogin } from "../MovieListContainer/actions";
import LoginForm from "../../components/LoginForm";





class LoginContainer extends React.Component {

    render() {
        
        return (
            <div>
                {
                    this.props.isLoggedIn 
                        ? <Redirect to="/movies" />
                        : <LoginForm error={this.props.error} requestLogin={this.props.requestLogin} />
                }
                
            </div>
        );
    }
}

function mapStateToProps(state) {
    return {
        isLoggedIn: state.MovieListContainerState.isLoggedIn,
        error: state.MovieListContainerState.error,
    };
}
function mapDispatchToProps(dispatch) {
    return {
        // login action to pass down to login form
        requestLogin: (user) => dispatch(requestLogin(user))
    };
}
export default connect(mapStateToProps, mapDispatchToProps)(LoginContainer);