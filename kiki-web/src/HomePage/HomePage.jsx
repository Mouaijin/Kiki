import React from 'react';

import { authenticationService } from "../_services/authentication.service";

export class HomePage extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            currentUser: authenticationService.currentUserValue
        };
    }

    componentDidMount() {
    }

    render() {
        console.log("render home");
        const { currentUser } = this.state;
        return (
            <div>
                <h1>Hi {currentUser.username}!</h1>
                <p>You're logged in with React & JWT!!</p>
            </div>
        );
    }
}
