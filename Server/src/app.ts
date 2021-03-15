import "reflect-metadata";
import { createExpressServer } from 'routing-controllers';
import mongoose from 'mongoose';

const app = createExpressServer({
    routePrefix: '/api',
    controllers: [__dirname + '/controllers/*.js'],
});

mongoose.connect('mongodb://localhost:27017/datalogger', {useNewUrlParser: true})
    .then(result => {
        app.listen(3000, () => {
            console.log('App listening on port 3000.');
        });
    })
    .catch(error => console.log(error));