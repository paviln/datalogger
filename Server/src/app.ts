import bodyParser from 'body-parser';
import express from 'express';
import helmet from 'helmet';
import mongoose from 'mongoose';
import multer from 'multer';
import dotenv from 'dotenv';

import routes from './routes';

dotenv.config({path: __dirname + '/../src/.env.exsample'});
const env = process.env;

const app = express();

// Parse body params and attache them to req.body.
app.use(bodyParser.json({ limit: '50mb' }));
app.use(bodyParser.urlencoded({ limit: '50mb', extended: true, parameterLimit: 50000 }));


app.use(helmet());

app.use('/api', routes);

// Make Mongoose use `findOneAndUpdate()`. Note that this option is `true`
// by default, you need to set it to false.
mongoose.set('useFindAndModify', false);

const conn = 'mongodb://'+env.MONGODB_HOST +':' +env.MONGODB_PORT + '/' + env.MONGODB_DATABASE;

mongoose.connect(conn, {useNewUrlParser: true})
    .then((result) => {
      app.listen(process.env.EXPRESS_PORT, () => {
        console.log('App listening on port ' + process.env.EXPRESS_PORT + '.');
      });
    })
    .catch((error) => console.log(error));