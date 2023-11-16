import Peer from 'simple-peer';

function createPeer(userToSignal, stream, sendSignal) {
  const peer = Peer({
    initiator: true,
    config: {
      iceServers: [],
    },
    trickle: false,
    stream,
  });

  peer.on('signal', (signal) => {
    sendSignal(signal, userToSignal, false);
  });
  console.log('Called');
  return peer;
}

function addPeer(incomingSignal, connectionId, stream, sendSignal) {
  const peer = new Peer({
    initiator: false,
    trickle: false,
    config: {
      iceServers: [],
    },
    stream,
  });

  peer.on('signal', (signal) => {
    sendSignal(signal, connectionId, true);
  });

  peer.signal(incomingSignal);

  return peer;
}

export default {
  createPeer,
  addPeer,
};
