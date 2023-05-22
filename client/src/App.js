import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import * as User from './pages/user/User'
import * as Odontologist from './pages/odontologist/Odontologist';
import * as Appointment from './pages/appointment/Appointment';
import { Navbar } from './common-components/Navbar/Navbar';
import { About } from "./pages/about/About"


export default function App() {
    return (
        <div>
            <Navbar />
            <div className="container-fluid">
                <BrowserRouter>
                    <Routes>
                        <Route path="" element={<About />} />

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
        </div>
    );
}
