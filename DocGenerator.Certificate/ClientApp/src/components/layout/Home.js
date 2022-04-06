import React, { Component} from 'react';
import './Home.css'
import { Courses } from '../fragments/Courses';
import ListCourses from '../../jsonCourses.json'


export class Home extends Component {

    constructor(props) {
        super(props);
    }

    render() {
        
        const ListComponents = [];        ListCourses.forEach((data) => {            ListComponents.push(<div className="col" style={{ paddingTop:'10px'}}>
                <div className="card h-100">
                    <Courses course={data}  />
                </div>
            </div>);
        });
        return (
            <div className="container">
                <div className="home-body">
                    <div className="row row-cols-1 row-cols-md-3 g-4" style={{ padding: '0px 20px 0px 20px' }}>
                        {ListComponents}
                    </div>
                </div>              
          </div >
    );
    }
}
