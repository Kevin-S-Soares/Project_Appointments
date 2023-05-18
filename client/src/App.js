import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import * as User from './pages/user/User'
import * as Odontologist from './pages/odontologist/Odontologist';
import * as Appointment from './pages/appointment/Appointment';


export default function App() {
    const url = window.location.origin;
    return (
        <div>
            <nav>
                <ul>
                    <label>User</label> 
                    <li><a href={url + "/User/Register"}>Register</a></li>
                    <li><a href={url + "/User/Login"}>Login</a></li>
                </ul>
                <ul>
                    <label>Odontologist</label>
                    <li><a href={url + "/Odontologist/Index"}>Index</a></li>
                </ul>
                <ul>
                    <label>Appointment</label>
                    <li><a href={url + "/Appointment/Index"}>Index</a></li>
                </ul>
            </nav>
            <BrowserRouter>
                <Routes>
                    <Route path="" element={<Odontologist.Index />} />

                    <Route path="/User/Login" element={<User.Login />} />
                    <Route path="/User/Register" element={<User.Register />} />
                    <Route path="/User/Verify" element={<User.Verify />} />
                    <Route path="/User/ForgetPassword" element={<User.ForgetPassword />} />
                    <Route path="/User/ResetPassword" element={<User.ResetPassword />} />

                    <Route path="/Odontologist/Index" element={<Odontologist.Index />} />
                    <Route path="/Odontologist/Add" element={<Odontologist.Add />} />
                    <Route path="/Odontologist/Edit/:id" element={<Odontologist.Edit />} />
                    <Route path="/Odontologist/Remove/:id" element={<Odontologist.Remove />} />
                    <Route path="/Odontologist/Details/:id" element={<Odontologist.Details />} />

                    <Route path="/Appointment/Add" element={<Appointment.Add />} />
                    <Route path="/Appointment/Index" element={<Appointment.Index />} />
                    <Route path="/Appointment/Edit/:id" element={<Appointment.Edit />} />
                    <Route path="/Appointment/Remove/:id" element={<Appointment.Remove />} />
                </Routes>
            </BrowserRouter>
        </div>
    );
}
