import { Controller, Param, Body, Get, Post, Put, Delete } from 'routing-controllers';
import mongoose from 'mongoose';

import Logger from '../models/logger';

@Controller('/logger')
export class LoggerController {
    @Post()
    save() {
        var logger:mongoose.Document = new Logger({ test: 'test'});
        logger.save();
        
        return { status: "success"};
    }
}