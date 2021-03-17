import express from "express";
import {create} from '../controllers/log.controller';

const router = express.Router();

router.route('/')
    .post(create);

export default router;