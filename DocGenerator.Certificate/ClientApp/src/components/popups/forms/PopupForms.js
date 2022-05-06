import React, {  Component } from 'react';
import './PopupForms.css'

export class PopupForms extends Component {

    static defaultProps = {
        course: {},
        closePopup: () => { },
    }

    constructor(props) {
        super(props);
    }



    render() {
        return (
            //Modal
            <div className="collapse show modal fade PopupForms" tabIndex="-1" style={{display:'block'}}>
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id="exampleModalLabel">Full Name</h5>
                        </div>
                        <div className="modal-body">
                            <div className="input-group">
                                <span className="input-group-text">Full Name</span>
                                <input type="text" className="form-control" placeholder="Example: Thiago Yuri Oliveira Monteiro" aria-label="Username" />
                            </div>
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-secondary" onClick={this.props.closePopup}>Close</button>
                            <button type="button" className="btn btn-primary" onClick={this.props.createCertificate}>Save changes</button>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
