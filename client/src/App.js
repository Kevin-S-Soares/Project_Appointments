import React from 'react';
import { BrowserRouter, Routes, Route} from 'react-router-dom';
import { AddOdontologist, IndexOdontologist, EditOdontologist, RemoveOdontologist, DetailsOdontologist } from './pages/odontologist/Odontologist';
import { LoginUser, RegisterUser, VerifyUser, ForgetPasswordUser, ResetPasswordUser } from './pages/user/User'

export default function App(){
    return(
        <BrowserRouter>
        <Routes>
            <Route path="" element={ <IndexOdontologist /> } />

            <Route path="/User/Login" element={ <LoginUser /> } />
            <Route path="/User/Register" element={ <RegisterUser /> } />
            <Route path="/User/Verify" element={ <VerifyUser /> } />
            <Route path="/User/ForgetPassword" element={ <ForgetPasswordUser /> } />
            <Route path="/User/ResetPassword" element={ <ResetPasswordUser /> } />

            <Route path="/Odontologist/Index" element={ <IndexOdontologist /> } />
            <Route path="/Odontologist/Add" element={ <AddOdontologist /> } />
            <Route path="/Odontologist/Edit/:id" element={ <EditOdontologist /> } />
            <Route path="/Odontologist/Remove/:id" element={ <RemoveOdontologist />} />
            <Route path="/Odontologist/Details/:id" element={ <DetailsOdontologist />} />
        </Routes>
        </BrowserRouter>

    );
}
