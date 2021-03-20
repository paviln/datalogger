import express from 'express';
import {create} from '../controllers/plant.controller';

// eslint-disable-next-line new-cap
const router = express.Router();

router.route('/')
    .post(create);

export default router;
