﻿const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    '/api/User/Login',
    '/api/User/Register',
    '/api/User/VerifyToken',
    '/api/User/ForgetPassword',
    '/api/User/ResetPassword',
    '/api/Odontologist'
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7156',
        secure: false
    });

    app.use(appProxy);
};
