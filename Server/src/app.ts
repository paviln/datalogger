import "reflect-metadata";
import { createExpressServer } from 'routing-controllers';
import mongose from 'mongoose';

const app = createExpressServer({
    routePrefix: '/api',
    controllers: [__dirname + '/controllers/*.js'],
});

mongose.connect('localhost')
    .then(result => {
        app.listen(3000, () => {
            console.log('App listening on port 3000.');
        });
    })
    .catch(error => console.log(error));