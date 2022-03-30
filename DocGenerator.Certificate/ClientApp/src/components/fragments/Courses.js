import React, { Component } from 'react';
import './Courses.css'

export class Courses extends Component {

    constructor(props) {
        super(props);
        this.state = props.course;
    }

    render() {
        return (
            <div>
                <div class="card" >
                    <img src={this.state.Image} class="card-img-top" id="image-course" alt={this.state.NameCourse} />
                    <div class="card-body">
                        <h5 class="card-title">{this.state.NameCourse}</h5>
                        <div class="card-text">{this.state.Description}</div>
                        <button class="btn btn-primary">Create certificate</button>
                        </div>
                </div>
            </div>
        );
    }
}
