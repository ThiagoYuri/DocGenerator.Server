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
                        <div class="card-text" style={{ height:'30px' }}>{this.state.Description}</div>
                    </div>
                    <div className="text-center aling-bottom" style={{ paddingBottom: '5px' }} >
                        <button class="btn btn-primary" style={{ width: '80%' }} >Create certificate</button>
                    </div>
                </div>
            </div>
        );
    }
}
