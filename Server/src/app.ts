import bodyParser from 'body-parser';
import express from 'express';
import helmet from 'helmet';
import mongoose from 'mongoose';

import routes from './routes'

const app = express();

// Parse body params and attache them to req.body.
app.use(bodyParser.urlencoded({extended : true}));
app.use(bodyParser.json());

app.use(helmet());

app.use('/api', routes);

// Make Mongoose use `findOneAndUpdate()`. Note that this option is `true`
// by default, you need to set it to false.
mongoose.set('useFindAndModify', false);

mongoose.connect('mongodb://localhost:27017/datalogger', {useNewUrlParser: true})
    .then(result => {
        app.listen(3000, () => {
            console.log('App listening on port 3000.');
        });
    })
    .catch(error => console.log(error));