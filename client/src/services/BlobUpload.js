

function uploadImageToAzure(file){
    const axios = require('axios');
    const server = "https://localhost:44378/api/uploads";
    // Call Back-end API to upload image 
    var formData = new FormData();
    formData.append("image",file);
    debugger;
    axios.post(
        server,
        formData,
        {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        }
    ).then(function(){
        alert('File Uploaded');
    }).catch(function() {
        alert('Failed');
    })
}

export default uploadImageToAzure;