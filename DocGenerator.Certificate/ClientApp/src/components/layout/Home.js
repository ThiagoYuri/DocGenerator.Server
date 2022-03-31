import React, { Component } from 'react';
import './Home.css'
import { Courses } from '../fragments/Courses';
import ListCourses from '../../jsonCourses.json'

export class Home extends Component {

    render() {
        const ListComponents = [];        ListCourses.forEach((data) => {            ListComponents.push(<div class="col" style={{ paddingTop:'10px'}}>
                <div class="card h-100">
                    <Courses course={data} />
                </div>
            </div>);
        });

        return (
            <div className="container">
                <div className="Header">
                    <div class="input-group">
                        <span class="input-group-text">Full Name</span>
                        <input type="text" class="form-control" placeholder="Example: Thiago Yuri Oliveira Monteiro" aria-label="Username"/>
                    </div>
                </div>

                <div class="row row-cols-1 row-cols-md-3 g-4" style={{ padding: '0px 20px 0px 20px' }}>
                        {ListComponents}                        
                </div>
          </div >
    );
    }
}
