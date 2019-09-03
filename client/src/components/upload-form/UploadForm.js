import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './UploadForm.css'
import uploadImageToAzure from '../../services/BlobUpload'
class UploadForm extends React.Component{

    constructor(props){
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.fileInput = React.createRef();
    }

    handleSubmit(){
        if(this.fileInput.current!=null){
            uploadImageToAzure(this.fileInput.current.files[0])
        }
    }

    render(){
        return (
            <div class="form-container">
                <form class="upload-form" onSubmit={this.handleSubmit}>
                    <div class="form-group">
                            <input id="fileInput" ref={this.fileInput} class="input-file" type="file" accept="image/x-png,image/gif,image/jpeg" style={{width: "95"}}/> <br/><br />
                            <button id="singlebutton" name="singlebutton" class="btn btn-primary" style={{width: "95%"}}>Submit</button>
                    </div>
                </form>
            </div>
        );
    }
}

export default UploadForm;