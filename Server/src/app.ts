import bodyParser from 'body-parser';
import express from 'express';
import helmet from 'helmet';
import mongoose from 'mongoose';
import dotenv from 'dotenv';

import routes from './routes'

dotenv.config({ path: __dirname + '/../src/.env'});

const app = express();

// Parse body params and attache them to req.body.
app.use(bodyParser.urlencoded({extended : true}));
app.use(bodyParser.json());

app.use(helmet());

app.use('/api', routes);

// Make Mongoose use `findOneAndUpdate()`. Note that this option is `true`
// by default, you need to set it to false.
mongoose.set('useFindAndModify', false);

mongoose.connect('mongodb://'+ process.env.MONGODB_HOST +':' + process.env.MONGODB_PORT + '/' + process.env.MONGODB_DATABASE, {useNewUrlParser: true})
    .then(result => {
        app.listen(process.env.EXPRESS_PORT, () => {
            console.log('App listening on port ' + process.env.EXPRESS_PORT + '.');
        });
    })
    .catch(error => console.log(error));