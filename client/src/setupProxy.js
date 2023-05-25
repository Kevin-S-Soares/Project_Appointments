const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    '/api/User/Login',
    '/api/User/Register',
    '/api/User/VerifyToken',
    '/api/User/ForgetPassword',
    '/api/User/ResetPassword',
    '/api/Odontologist',
    '/api/Appointment',
    '/api/DetailedOdontologist'
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'project-appointments.azurewebsites.net',
        secure: false
    });

    app.use(appProxy);
};
