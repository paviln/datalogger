import { Controller, Param, Body, Get, Post, Put, Delete } from 'routing-controllers';

@Controller('/logger')
export class LoggerController {
    @Get()
    getAll() {
        return 'This action returns all loggers';
    }
}