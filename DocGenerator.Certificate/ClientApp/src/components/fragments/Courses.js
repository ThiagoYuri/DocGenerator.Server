import React, { Component } from 'react';
import './Courses.css'
import { PopupForms } from '../popups/forms/PopupForms'

export class Courses extends Component {

    constructor(props) {
        super(props);
        this.state = { isOpenModel: false };
        this.updateStateModal = this.updateStateModal.bind(this)
    }

    //Create or destroy PopupModal
    updateStateModal() {
        if (this.state.isOpenModel)
            this.setState({ isOpenModel: false });
        else 
            this.setState({ isOpenModel: true });        
    }

    render() {
        const { props } = this,
            { course } = props;
        const { isOpenModel } = this.state
        return (
                <div className="card center" >
                    <div id="color-background-img">
                        <div className='center' style={{height:'70%',width:'70%'}}>
                            <img src={course.Image}  id="image-course" alt={course.NameCourse} />
                        </div>
                    </div>
                    <div className="card-body">
                        <h5 className="card-title">{course.NameCourse}</h5>
                        <div className="card-text" style={{ height:'30px' }}>{course.Description}</div>
                    </div>
                    <div className="text-center aling-bottom" style={{ paddingBottom: '5px' }} >
                        <button className="btn btn-primary" style={{ width: '80%' }} onClick={this.updateStateModal} >Create certificate</button>
                    </div>
                {(isOpenModel ? <PopupForms className='PopupForms' closePopup={this.updateStateModal} /> : null)}
                </div>
        );
    }
}
