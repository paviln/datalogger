import mongoose, { Document, Schema } from 'mongoose';

export class Logger {
    minimum_temperature: number;
    logs: mongoose.Schema.Types.ObjectId[];
    plants: mongoose.Schema.Types.ObjectId[];
}

export interface ILogger extends Document {
    minimum_temperature: Number,
    logs: mongoose.Schema.Types.ObjectId[],
    plants: mongoose.Schema.Types.ObjectId[],
}

const LoggerSchema = new mongoose.Schema({
    minimum_temperature: String,
    logs: [{
        type: Schema.Types.ObjectId,
        ref: 'Logs'
    }],
    plants: [{
        type: Schema.Types.ObjectId,
        ref: 'Plants'
    }]
});

export const Loggers =  mongoose.model<ILogger>('Loggers', LoggerSchema);