import mongoose, { Document } from 'mongoose';

export class Log {
    temperature: number;
    air_humidity: number;
    soil_humidity: number;
    timestamp: Date;
    loggerId: mongoose.Schema.Types.ObjectId;
}

export interface ILog extends Document {
    temperature: Number,
    air_humidity: Number,
    soil_humidity: Number,
    timestamp: Date,
    loggerId: mongoose.Schema.Types.ObjectId
}

const LogSchema = new mongoose.Schema({
    temperature: Number,
    air_humidity: Number,
    soil_humidity: Number,
    timestamp: Date,
    loggerId: { type: mongoose.Schema.Types.ObjectId }
});

export const Logs = mongoose.model<ILog>('Log', LogSchema);