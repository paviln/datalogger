import mongoose from 'mongoose';

const PlantSchema = new mongoose.Schema({
    test: String,
    loggerId: mongoose.Types._ObjectId
});

export default mongoose.model('Plant', PlantSchema);