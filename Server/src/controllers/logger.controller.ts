import { JsonController, Param, Body, Get, Post, Put, Delete } from 'routing-controllers';

import { ILogger, Loggers } from '../models/logger';

@JsonController('/logger')
export class LoggerController {
    @Post()
    post(@Body() logger: ILogger) {
        Loggers.create(logger, (err, small) => {
            if (err) return "lol"
        });
    }
}