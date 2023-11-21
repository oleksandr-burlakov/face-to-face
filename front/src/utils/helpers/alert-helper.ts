import Swal, { SweetAlertOptions } from 'sweetalert2';

const successAlert = (text: string, title?: string) => {
    Swal.fire({  
        title,  
        text,
        icon: 'success'
      }); 
}

const timeSuccessAlert = (text: string, title?: string) => {
    Swal.fire({  
        title,  
        text,
        icon: 'success',
        timer: 1000,
        showConfirmButton: false
      }); 
}
const timeErrorAlert = (text: string, title?: string) => {
    const configuration: SweetAlertOptions = {
        title,
        text,
        icon: 'error',
        timer: 2000,
        showConfirmButton: false
    };
    Swal.fire(configuration); 
}

const errorAlert = (text: string, title?: string) => {
    const configuration: SweetAlertOptions = {
        title,
        text,
        icon: 'error'
    };
    Swal.fire(configuration); 
}

export {errorAlert, successAlert, timeErrorAlert, timeSuccessAlert};