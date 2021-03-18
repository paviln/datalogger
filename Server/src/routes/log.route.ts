import express from 'express';
import {create} from '../controllers/log.controller';

// eslint-disable-next-line new-cap
const router = express.Router();

router.route('/')
    .post(create);

export default router;
