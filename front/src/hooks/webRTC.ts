import { useRef, useEffect } from 'react';

import Peer, { PeerEvents, PeerOptions } from 'peer-lite';

export function useCreatePeer(options: PeerOptions = {}): Peer {
  const peerRef = useRef<Peer>();

  if (!peerRef.current) {
    peerRef.current = new Peer(options);
  }

  useEffect(
    () => () => {
      peerRef.current?.destroy();
    },
    []
  );

  return peerRef.current;
}

export function usePeer<E extends keyof PeerEvents>(peer: Peer, eventName: E, func: PeerEvents[E]) {
  useEffect(() => {
    peer.on(eventName, func);
    return () => {
      peer.off(eventName, func);
    };
  }, []);
}